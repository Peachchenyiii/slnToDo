// 以上預設(引用/存取)命名空間
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// 為了存取在Model資料夾裡db.ToFo.mdf資料庫的db.ToDoEntites而引用prjToDo.Model命名空間
using prjToDo.Models;

// 使用命名空間控制範圍，namespace關鍵字用來宣告範圍。
// 在專案內建立範圍的能力有助於組織程式碼，並可讓您建立全域唯一類型。
namespace prjToDo.Controllers
{
    // 宣告類別為public class，class為參考型別
    // HomeController繼承單一的Controller
    public class HomeController : Controller
    {
        // 建立型別通常會new運算子的建構函式
        // 建立dbToDoEntities類別db物件
        dbToDoEntities db = new dbToDoEntities();

        // GET: Home
        public ActionResult Index()
        {
            // todos資料表內的紀錄依fDate欄位進行遞減排序並轉成串列再將結果指定todos變數
            // OrderByDescending設定第一個排序條件，而且此排序條件為遞減排序。
            var todos = db.tToDo.OrderByDescending(m => m.fDate).ToList();
            // 將todos代辦事項的所有紀錄傳到Index.cshtml的View檢視頁面
            return View(todos);
        }
         
        // 第33-36行，連結到Home/Create時會執行此方法，接著傳回預設的Create.cshtml的View檢視頁面
        public ActionResult Create()
        {
            return View();
        }

        // 第39-55行，在Create.cshtml的檢視頁面按下Submit鈕會執行此方法。
        [HttpPost]
        public ActionResult Create(string fTitle, string fImage, DateTime fDate)
        {
            // 建立tToDo待辦資料型別todo物件
            tToDo todo = new tToDo();
            // 將表單fTitle、fImage、fDate欄位的資料逐一指定給todo物件的fTitle、fImage、fDate屬性
            // 此處未指定fId欄位資料是因為tToDo資料表已設定fId欄位為自動編號(識別規格)，因此fId欄位會採取自動編號模式由1開始新增
            todo.fTitle = fTitle;
            todo.fImage = fImage;
            todo.fDate = fDate;
            // 將todo待辦事項物件放入tToDo資料表內
            db.tToDo.Add(todo);
            // 執行SaveChanges()方法進行異動資料庫，必須執行此方法todo待辦事項物件才可寫入tToDo資料表內
            db.SaveChanges();
            // 重新執行Home/Index()方法，將todos待辦事項結果傳給Index.cshtml的View檢視頁面
            return RedirectToAction("Index");
        }

        // 第58-68行，連結到Home/Delete並傳入URL的id參數時會執行此方法
        public ActionResult Delete(int id)
        {
            // 依傳入的id參數找出要刪除的待辦事項物件並指定給todo
            var todo = db.tToDo.Where(m => m.fId == id).FirstOrDefault();
            // 刪除tToDo資料表符合todo物件的資料
            db.tToDo.Remove(todo);
            // 執行SaveChanges()方法進行異動資料庫，必須執行此方法才能刪除tToDo資料表內符合todo物件的記錄
            db.SaveChanges();
            // 重新執行Home/Index()方法
            return RedirectToAction("Index");
        }

        // 第72-76，連結到Home/Edit並傳入修改的URL參數id編號會執行此方法，接著找出欲修改的待辦事項物件todo，
        // 再將要修改的待辦事項物件todo傳入Edit.cshtml的View檢視頁面
        public ActionResult Edit(int id)
        {
            var todo = db.tToDo.Where(m => m.fId == id).FirstOrDefault();
            return View(todo);
        }

        // 第-行，在Edit.cshtml的檢視頁面按下Submit鈕會執行此方法
        [HttpPost]
        public ActionResult Edit(int fId, string fTitle, string fImage, DateTime fDate)
        {
            // 依fId取得要修改的待辦事項todo
            var todo = db.tToDo.Where(m => m.fId == fId).FirstOrDefault();
            // 將表單fTitle、fImage、fDate欄位的資料逐一指定給欲修改todo物件的fTitle、fImage、fDate屬性
            todo.fTitle = fTitle;
            todo.fImage = fImage;
            todo.fDate = fDate;
            // 執行SaveChanges()方法進行異動資料庫，必須執行此方法tToDo待辦事項物件修改寫回todo資料表內
            db.SaveChanges();
            // 重新執行Home/Index()方法，顯示待辦事項列表面
            return RedirectToAction("Index");
        }
    }
}