# dashboard2025
-- สร้างตาราง SectionTargets เป็นตารางสำหรับเก็บค่าเป้าหมายของ oee และ  eii ในแต่ละปี
CREATE TABLE SectionTargets (
    SectionTargetId INT IDENTITY(1,1) PRIMARY KEY,
    OeeYear INT NOT NULL,
    OeeTarget DECIMAL(18, 2) NOT NULL,
    GiveAwayMin DECIMAL(18, 3) NOT NULL,
    GiveAwayMax DECIMAL(18, 3) NOT NULL
);

คำอธิบาย:
CREATE TABLE SectionTargets: คำสั่งเริ่มต้นในการสร้างตารางใหม่ชื่อ SectionTargets
SectionTargetId INT IDENTITY(1,1) PRIMARY KEY:
    SectionTargetId INT: กำหนดคอลัมน์ SectionTargetId ให้เป็นชนิดข้อมูลจำนวนเต็ม (INT)
    IDENTITY(1,1): กำหนดให้คอลัมน์นี้เป็น Identity Column ซึ่งจะสร้างเลขลำดับอัตโนมัติ โดยเริ่มต้นที่ 1 และเพิ่มขึ้นทีละ 1 สำหรับแต่ละแถวที่ถูกเพิ่มเข้าไปในตาราง
    PRIMARY KEY: กำหนดให้คอลัมน์นี้เป็น Primary Key ของตาราง ซึ่งหมายความว่าค่าในคอลัมน์นี้จะต้องไม่ซ้ำกันและไม่เป็นค่าว่าง (NULL)
OeeYear INT NOT NULL:
    OeeYear INT: กำหนดคอลัมน์ OeeYear ให้เป็นชนิดข้อมูลจำนวนเต็ม (INT)
    NOT NULL: กำหนดให้คอลัมน์นี้ต้องมีค่าเสมอ ห้ามเป็นค่าว่าง (NULL)
OeeTarget DECIMAL(18, 2) NOT NULL:
    OeeTarget DECIMAL(18, 2): กำหนดคอลัมน์ OeeTarget ให้เป็นชนิดข้อมูลทศนิยมที่มีความแม่นยำรวม 18 หลัก และมีทศนิยม 2 ตำแหน่ง
    NOT NULL: กำหนดให้คอลัมน์นี้ต้องมีค่าเสมอ ห้ามเป็นค่าว่าง (NULL)
GiveAwayMin DECIMAL(18, 3) NOT NULL:
    GiveAwayMin DECIMAL(18, 3): กำหนดคอลัมน์ GiveAwayMin ให้เป็นชนิดข้อมูลทศนิยมที่มีความแม่นยำรวม 18 หลัก และมีทศนิยม 3 ตำแหน่ง
    NOT NULL: กำหนดให้คอลัมน์นี้ต้องมีค่าเสมอ ห้ามเป็นค่าว่าง (NULL)
GiveAwayMax DECIMAL(18, 3) NOT NULL:
    GiveAwayMax DECIMAL(18, 3): กำหนดคอลัมน์ GiveAwayMax ให้เป็นชนิดข้อมูลทศนิยมที่มีความแม่นยำรวม 18 หลัก และมีทศนิยม 3 ตำแหน่ง
    NOT NULL: กำหนดให้คอลัมน์นี้ต้องมีค่าเสมอ ห้ามเป็นค่าว่าง (NULL)

# ตัวอย่าง data OEE 1 วัน แต่การส่งออกไป font-end จะต้องมี 30 วันเสมอ ดูจาก class MachineOEEData เฉพาะ bagging PL จะมีทั้งหมด 9 List เครื่องจักร
{
  "data": [
    {
      "date": "03-01-2025",
      "MachineName": "PP12/A",
      "oee": 75,
      "availability": 0.80,
      "performance": 0.90,
      "quality": 0.95,
      "oeetarget": 85,

      "remark": [
        "ปัญหาการตั้งค่าเครื่องจักร",
        "วัตถุดิบไม่ได้มาตรฐาน",
        "หยุดทำงานชั่วคราว"
      ],
      "color": "#FF6600",
      "titleoee": "OEE Chart for Machine PP12/A",
      "titlgiveaway": "Give Away Machine PP12/A",
      "targetgiveaway": "Give Away Machine PP12/A",


    }
  ]
  
}