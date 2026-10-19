using Microsoft.AspNetCore.Mvc;
using wt_lab4_4mvc_denisyuk.Models;

namespace wt_lab4_4mvc_denisyuk.Controllers;

public class BookController : Controller
{
    // Создаем коллекцию из 5 объектов (выполнение на 3 балла Блока 3)
    private static readonly List<Book> Books = new()
    {
        new Book { Id = 1, Title = "Война и мир", Author = "Лев Толстой", Year = 1869, IsAvailable = true },
        new Book { Id = 2, Title = "Преступление и наказание", Author = "Федор Достоевский", Year = 1866, IsAvailable = true },
        new Book { Id = 3, Title = "Мастер и Маргарита", Author = "Михаил Булгаков", Year = 1967, IsAvailable = false },
        new Book { Id = 4, Title = "1984", Author = "Джордж Оруэлл", Year = 1949, IsAvailable = true },
        new Book { Id = 5, Title = "Гарри Поттер и философский камень", Author = "Джоан Роулинг", Year = 1997, IsAvailable = false }
    };

    // Action для отображения списка
    public IActionResult Index()
    {
        return View(Books);
    }
        public IActionResult Details(int id)
    {
        var book = Books.FirstOrDefault(b => b.Id == id);
        if (book == null) return NotFound(); // Если книги с таким ID нет, выдаст 404
        return View(book);
    }
        // GET: /Book/Edit/1
    public IActionResult Edit(int id)
    {
        var book = Books.FirstOrDefault(b => b.Id == id);
        if (book == null) return NotFound();
        return View(book);
    }

    // POST: /Book/Edit/1
    [HttpPost]
    public IActionResult Edit(int id, Book book)
    {
        if (id != book.Id) return NotFound();

        if (ModelState.IsValid)
        {
            var existingBook = Books.FirstOrDefault(b => b.Id == id);
            if (existingBook != null)
            {
                // Обновляем данные
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Year = book.Year;
                existingBook.IsAvailable = book.IsAvailable;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(book);
    }
       // POST: /Book/Delete/1
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var book = Books.FirstOrDefault(b => b.Id == id);
        if (book != null)
        {
            Books.Remove(book);
        }
        return RedirectToAction(nameof(Index));
    }
     // GET: /Book/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Book/Create
    [HttpPost]
    public IActionResult Create(Book book)
    {
        if (ModelState.IsValid)
        {
            // Автоматически генерируем ID для новой книги
            book.Id = Books.Any() ? Books.Max(b => b.Id) + 1 : 1;
            Books.Add(book);
            return RedirectToAction(nameof(Index));
        }
        return View(book);
    }
}