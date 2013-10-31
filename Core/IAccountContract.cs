using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Component.Tools;
using Core.Models;

namespace Core
{
    /// <summary>
    /// 账户模块核心业务契约 业务操作结果
    /// </summary>
    public interface IAccountContract
    {
        #region 属性

        IQueryable<Member> Members { get; }
        IQueryable<LoginLog> LoginLogs { get; }
        IQueryable<Product> Products { get; }
        IQueryable<Category> Categorys { get; }

        #endregion
        int UpEntity(Member entity);

        int Add(Product product);
        int Add(Category category);
        int DeleteProduct(int[] list);
        int DeleteProduct(IEnumerable<Product> products);
        int DeleteCategory(int[] list);
        int DeleteCategory(int index);
        int DeleteCategory(Category category);
        int UpCategory(Category cat);
        int UpProduct(Product product);
        int UpProduct(IEnumerable<Product> products);
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginInfo">登录信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult Login(LoginInfo loginInfo);
    }
}
