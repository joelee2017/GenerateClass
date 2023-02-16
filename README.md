# GenerateClass
使用keyword 方式生成帶 namespace、summary、Description、property 類別程式

- 新增、修改、刪除功能
- 型別選擇器、並指定是否為 null
- 程序可持續循環直到使用結束鍵

使用方法：

1. 請先修改 filePath 輸出路徑
2. 輸入類別名稱
3. 輸入屬性名稱
4. 輸入屬性類型
5. 選擇屬性是否為null
6. 輸入屬性描述
7. 完成後按 Enter 或繼續輸入屬性，輸入相同名稱屬性則為修改或刪除
8. 確認後按 Enter 產生類別名稱.cs 檔案

支援型別：

0：bool
1：byte
2：sbyte
3：char
4：decimal
5：double
6：float
7：int
8：uint
9：short
10：ushort
11：object
12：string