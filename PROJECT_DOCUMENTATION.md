# BeastWar (12 Tails Port to Unity 6) - Project Documentation & Changelog

เอกสารบันทึกรายละเอียดการดำเนินงาน การแก้ปัญหา การปรับโครงสร้างโค้ด (Refactoring) และการอัปเกรดระบบทั้งหมดของโปรเจกต์ **BeastWar**

---

## 1. ภาพรวมโปรเจกต์ (Project Overview)
* **เป้าหมาย:** สกัด Asset และโค้ดจากตัวเกม **12 Tails Online (Offline Client Build)** แล้วนำมาพอร์ต อัปเกรด และพัฒนาต่อบน **Unity 6 (6000.3.0f1) + Universal Render Pipeline (URP)**
* **GitHub Repository:** [https://github.com/QNIIKE1234/BeastWar.git](https://github.com/QNIIKE1234/BeastWar.git)
* **Branch หลัก:** `main`
* **ที่ตั้งโปรเจกต์:** `D:\Unity Game Project\Eruption\BeastWar\BeastWar`

---

## 2. การสกัด Asset และโครงสร้างพื้นฐาน (Asset Extraction & Setup)
1. **การดึงข้อมูลตัวเกม:**
   * สกัดข้อมูลจาก Build ออฟไลน์ด้วย AssetRipper (51,320 ไฟล์, รวมขนาดกว่า 1.5 GB)
   * นำเข้าสู่ Unity ครบถ้วนทั้ง Meshes (2,054 ชิ้น), Textures, Materials, Shaders, Animations, เสียง (AudioClips), 15 Scenes, และ C# Scripts จำนวน 66 ไฟล์
2. **การตั้งค่า Project Settings ให้เข้ากับ Unity 6:**
   * `ProjectSettings.asset`: เปิด **`activeInputHandler: 2` (Both)** เพื่อให้ระบบรองรับทั้ง New Input System ของ Unity 6 และ Legacy Input (`Input.GetAxis`, `Input.GetKeyDown`) ของโค้ดเดิม
   * `EditorBuildSettings.asset`: ลงทะเบียนทั้ง 15 Scenes เข้าสู่ Build Settings เรียงลำดับตาม Index
   * `TagManager.asset`: เพิ่ม Tags สำคัญของเกมที่หลุดไป ได้แก่ `NPC`, `Enemy`, `Icon`, `Projectile`, `Item`
   * `.gitignore`: ตั้งค่าละเว้นโฟลเดอร์ Cache ขนาดใหญ่ของ Unity (`Library/`, `Temp/`, `Logs/`, `UserSettings/`)
3. **การแก้ปัญหา DLL ชนกันและ Error จาก Unity ยุคเก่า:**
   * ย้าย `Unity.TextMeshPro.dll` และ `Unity.Timeline.dll` รุ่นเก่าออกจาก `Assets/Plugins/` ไปยังโฟลเดอร์ Backup เพื่อให้โปรเจกต์ใช้เวอร์ชันทางการของ Unity 6 Package (`com.unity.ugui`)
   * แยกสคริปต์ตัวอย่างของ TMP โบราณที่ใช้โค้ด UV เก่า (`Vector2[]` vs `Vector4[]`) ออก
   * แก้ไขปัญหา Class `Selection` ของตัวเกมที่ไปบดบัง `UnityEditor.Selection` ใน Readme Editor

---

## 3. การออกแบบและแยกส่วนสถาปัตยกรรม (Manager Architecture)
แยกความรับผิดชอบของระบบต่างๆ ออกเป็น **Manager ประจำ Scene** อย่างชัดเจน โดยมีระบบ **Self-Healing / Auto-Init** ทำงานได้ทันทีแม้กด Play ใน Editor:

### 1) `LoginManager.cs` (ฉาก `A01_SecureLogin`)
* บริหารจัดการระบบ Authentication ของหน้า Login
* บันทึกและโหลดชื่อครอบครัว (`fam_name`) ผ่าน `PlayerPrefs`
* รองรับคีย์ลัด **Enter / Keypad Enter / Space** ในการกดล็อกอินทันที
* แปลงปุ่มกดเดิม (`Login_h`) ที่เคยใช้สคริปต์ดักคลิก `button_hold` ให้เป็นปุ่ม `UnityEngine.UI.Button` มาตรฐาน
* จัดการ Transition หน้าจอ (`Initiate.Fade`) เพื่อเปลี่ยนฉากเข้าสู่ `L10_LobbyGuild`

### 2) `LobbyManager.cs` (ฉาก `L10_LobbyGuild`)
* ควบคุมการทำงานของหน้าเลือกตัวละคร (Character Select) และหน้าสร้างตัวละคร (Character Create)
* ผูก Event ให้กับปุ่มทั้งหมด:
  * `b_arrow_l` / `b_arrow_r` : วนสลับเลือกตัวละครซ้าย-ขวา
  * `sel_b` : เลือกตัวละครและเริ่มเกม
  * `botton_create_char` : สลับเข้าสู่หน้าต่างสร้างตัวละคร
  * `Create_button` : ยืนยันการสร้างตัวละคร
  * `Cancel_create_char` : ยกเลิกและกลับมาหน้าเลือก
  * `del_b` : ลบตัวละครในช่องที่เลือก
  * `button_random_face` : สุ่มหน้าตาตัวละคร
* **Offline Player Mocking:** หากผู้เล่นกดเริ่มเกมโดยที่ยังไม่ได้สร้างตัวละคร ระบบจะจัดเตรียมตัวละครเริ่มต้นชื่อ `"Hero"` ให้ทันที เพื่อให้สามารถเข้าไปทดสอบฉากอื่นๆ ได้โดยไม่ค้าง
* เปลี่ยนผ่านเข้าสู่ฉากทุ่งหญ้าเริ่มต้น (`G31_GuildPlain_n`)

### 3) `GameSceneManager.cs` (ฉากเมือง `T51`–`T56` และแคมป์ผจญภัย `G30`–`G36`)
* จัดการฉาก Gameplay และฉากเมืองทั้งหมด
* **Offline Spawner:** หากโหลดเข้าฉากใดฉากหนึ่งใน Editor โดยตรง ระบบจะตรวจสอบหาตัวละคร `Player` หากไม่พบจะสั่งเกิดตัวละครและผูกมุมกล้อง `CameraControl` / `TownCamera` ให้อัตโนมัติ
* **Live TMPro HUD:** ควบคุมและอัปเดตข้อมูลบนหน้าจอ HUD (`GameGui.prefab`) แบบ Real-time:
  * ชื่อตัวละคร และ เลเวล (`CharacterNameLv`)
  * ชื่อตระกูล (`FamilyName`)
  * กล่องชื่อตัวละคร (`char_name_box`)
  * ปริมาณเลือด (`HP_Text`)
* **In-Game Escape Menu:** เมื่อกดปุ่ม `ESC` จะเปิดหน้าต่าง Option Menu ให้เลือก (Resume, กลับสู่ Lobby, กลับสู่ Login, หรือออกเกม)

---

## 4. การอัปเกรด UI เป็น Modern TextMeshPro (TMPro)
ทำการลบและแทนที่ Component เก่าที่เป็น Legacy UI ทั้งหมดให้เป็น TextMeshPro คุณภาพสูง:

1. **ฉาก Login (`A01_SecureLogin`):**
   * แปลง `User_Name` และ `Password` เป็น **`TMP_InputField`**
   * แปลง Child `Text` และ `Placeholder` ทั้งหมดเป็น **`TextMeshProUGUI` (`TextMeshPro - Text (UI)`)**
   * ผูกฟอนต์คมชัดระดับ Vector **`LiberationSans SDF`**
   * ช่อง `Password` เปิดโหมด Password Masking
2. **ฉาก Lobby (`L10_LobbyGuild` & `Create_button.cs`):**
   * ช่องกรอกชื่อตัวละคร `Character_Name` แปลงเป็น `TMP_InputField` และ `TextMeshProUGUI`
   * ปรับปรุงสคริปต์ `Create_button.cs` ให้ดึงชื่อตัวละครจาก TMPro ได้โดยตรง
3. **In-Game HUD (`GameGui.prefab`):**
   * แก้ไขไฟล์ Prefab เชื่อมโยงตัวแสดงผลข้อความ (`FamilyName`, `CharacterNameLv`, `char_name_box`, `Race`, `Guild_n`, `Rp`, `Mrp`) จากเดิมที่ติด Error Missing Script จาก DLL โบราณ ให้กลายเป็น `TextMeshProUGUI` ของ Unity 6 อย่างสมบูรณ์
4. **ระบบเรือบิน / วาร์ป (`btn_tsp.cs` และ `takeoff_tsp.cs`):**
   * เพิ่มฟังก์ชัน Self-Healing ผูก Component `UnityEngine.UI.Button` และแก้ปัญหา Raycast Target ไม่ทำงานใน New Input System
5. **Universal Tool (`SceneUIUpgrader.cs`):**
   * สร้างสคริปต์ Editor พร้อมเมนูด้านบนของ Unity (`12Tails`) เพื่อให้อัปเกรดทุก Scene หรือเลือกอัปเกรดทีละ Scene ได้ทันที:
     * `12Tails ➔ Upgrade All Scenes to Modern UI (TMPro)`
     * `12Tails ➔ Upgrade Login Scene (A01_SecureLogin)`
     * `12Tails ➔ Upgrade Lobby Scene (L10_LobbyGuild)`

---

## 5. ข้อมูลฉากและระบบดันเจี้ยน (Scene & Dungeon Analysis)

### รายชื่อ 15 Scenes ในโปรเจกต์:
| หมวดหมู่ | ชื่อ Scene | รายละเอียด |
| :--- | :--- | :--- |
| **System** | `A01_SecureLogin` | หน้าต่างเข้าสู่ระบบ (Login) |
| **Lobby** | `L10_LobbyGuild` | หน้าเลือกและสร้างตัวละคร (Character Select & Create) |
| **Light City** | `T51_MainStreet` | เมืองแสง: ถนนสายหลัก |
| | `T52_CentralSquare` | เมืองแสง: ลานกลางเมือง |
| | `T53_CityLibrary` | เมืองแสง: ห้องสมุดเมือง |
| | `T54_LightPark` | เมืองแสง: สวนสาธารณะ |
| | `T55_LightPalace` | เมืองแสง: พระราชวัง |
| **Arena** | `T56_PlainColosseum` | ลานประลอง Colosseum (ฉากต่อสู้ / Raid Boss Arena) |
| **Camps / Plains**| `G30_NoGuild` | แคมป์อิสระ |
| | `G31_GuildPlain_n` | ทุ่งหญ้ากิลด์ (พื้นที่ Chapter 1) |
| | `G32_SnowCamp` | แคมป์หิมะ (พื้นที่ Chapter 2) |
| | `G33_CrystalBeach` | หาดคริสตัล (พื้นที่ Chapter 3) |
| | `G34_ExcavationCamp` | แคมป์ขุดค้น (พื้นที่ Chapter 4) |
| | `G35_UndergroundCamp` | แคมป์ถ้ำใต้ดิน (พื้นที่ Chapter 5) |
| | `G36_ForestCamp` | แคมป์ป่าลึก (พื้นที่ Chapter 6) |

### สถาปัตยกรรมดันเจี้ยนใน 12 Tails:
* ตัวเกมไม่มี Scene ที่ตั้งชื่อว่ามิชชั่นเดี่ยวๆ (`Mission 1/1`) แต่ใช้ **Shared Terrain ของแคมป์/ทุ่งหญ้า (`G31`–`G36`) หรือลานประลอง (`T56_PlainColosseum`)** ในการทำมิชชั่น
* ระบบมิชชั่นจะใช้ **Wave Spawner** ในการดึงโมเดลและแอนิเมชันของมอนสเตอร์จาก `Assets/Resources/gameassets/characters/` มาเกิดตามพิกัด
* **คลัง Asset มอนสเตอร์พร้อมใช้งาน 100%:**
  * **Bugs:** `fatbug`, `littlefatbug`, `needlebug`, `sandbug`, `crystalbug`, `coralbug`, `goldenkingbug`, `silverqueenbug`, `stingqueen`
  * **Elementals & Bosses:** `kingrodinia`, `icedemon`, `iceguardian`, `lightgolem`, `boronknight`, `chromeknight`, `phoenix`, `zerbiras`
  * **Plants:** `carron`, `madcarron`, `cacton`, `mupo`, `peppon`, `pumpkin`, `pumpkonking`
  * **Robots:** `devilkaiser`, `kingkaiser`, `gallonbot`, `giantgallonbot`, `firebot`, `carrierbot`
  * ทุกตัวมี Prefab, Model, Bone Rigging, และ Animations (Walk, Attack, Hit, Die) ครบถ้วน

---

## 6. สรุปประวัติการ Commit สำคัญ (Git History)
* `26358c0f` : Initial commit: 12 Tails Unity project for BeastWar (51,320 files ported to Unity 6)
* `073818ae` : Refactor Login UI: Auto-upgrader for TextMeshPro, TMP_InputField and modern LoginManager
* `033e9c32` : Improve LoginSceneUpgrader auto-check loop and dirty tracking
* `b190fc0a` : Refactor scenes: LobbyManager, GameSceneManager, modern TMPro HUD, and SceneUIUpgrader
