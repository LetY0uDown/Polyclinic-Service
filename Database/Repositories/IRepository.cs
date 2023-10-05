using System.Linq.Expressions;
using Tools.Flags;

namespace Database.Repositories;

/// <summary>
/// Интерфейс для инкапсуляции прямого взаимодействия с Базой Данных
/// </summary>
/// <typeparam name="T">Класс модели</typeparam>
public interface IRepository<T> where T : IEntityModel
{
    /// <summary>
    /// </summary>
    /// <returns>Все записи из соответствующей таблицы БД</returns>
    Task<List<T>> GetAllAsync ();

    /// <summary>
    /// Добавляет запись в таблицу БД
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>Запись из Базы Данных</returns>
    Task<T> AddAsync (T entity);

    /// <summary>
    /// Обновляет запись в Базе Данных
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task UpdateAsync (T entity);

    /// <summary>
    /// Удаляет соответствующую запись из Базы Данных
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task DeleteAsync (T entity);

    /// <summary>
    /// Находит запись в таблице БД по её ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Найденную запись или null</returns>
    Task<T?> FindAsync (object id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="func"></param>
    /// <returns>Имеется ли в БД запись, подходящая к данному условию</returns>
    Task<bool> AnyAsync (Expression<Func<T, bool>> func);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="func"></param>
    /// <returns>Первую запись из таблицы, которая соответствует данному условию или null</returns>
    Task<T?> FirstOrDefaultAsync (Expression<Func<T, bool>> func);

    Task<List<T>> WhereAsync (Expression<Func<T, bool>> func);
}