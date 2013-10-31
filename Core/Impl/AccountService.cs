using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

using Component.Tools;
using Core;
using Core.Models;
using Core.Data.Repositories;

namespace Core.Impl
{
    /// <summary>
    /// 账户模块核心业务实现
    /// </summary>
    public abstract class AccountService:CoreServiceBase,IAccountContract
    {
        [Import]
        protected IMemberRepository MemberRepository { get; set; }
        [Import]
        protected ILoginLogRepository LoginLogRepository { get; set; }
        [Import]
        protected IProductRepository ProductRepository { get; set; }
        [Import]
        protected ICategoryRepository CategoryRepository { get; set; }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginInfo">登录信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult Login(LoginInfo loginInfo)
        {
            Member member = MemberRepository.Entities.SingleOrDefault(m => m.UserName == loginInfo.Access || m.Email == loginInfo.Access);
            if (member == null)
            {
                return new OperationResult(OperationResultType.QueryNull, "指定账号的用户不存在。");
            }
            if (member.Password != loginInfo.Password)
            {
                return new OperationResult(OperationResultType.Warning, "登录密码不正确。");
            }
            LoginLog loginLog = new LoginLog { IpAddress = loginInfo.IpAddress, Member = member };
            LoginLogRepository.Insert(loginLog);
            return new OperationResult(OperationResultType.Success, "登录成功。", member);
        }

        public IQueryable<Member> Members
        {
            get { return MemberRepository.Entities; }
        }

        public IQueryable<LoginLog> LoginLogs
        {
            get { return LoginLogRepository.Entities; }
        }

        public IQueryable<Product> Products
        {
            get { return ProductRepository.Entities; }
        }

        public IQueryable<Category> Categorys
        {
            get { return CategoryRepository.Entities; }
        }

        public int UpEntity(Member entity)
        {
            return MemberRepository.Update(entity);
        }


        public int Add(Product product)
        {
            return ProductRepository.Insert(product);
        }
        public int Add(Category ctegory)
        {
            return CategoryRepository.Insert(ctegory);
        }
        public int DeleteProduct(int[] list)
        {
            List<Product> products = new List<Product>();
            foreach (var item in list)
            {
                Product ps = ProductRepository.Entities.SingleOrDefault(p => p.Id == item);
                if (ps != null)
                {
                    products.Add(ps);
                    MvcUploader.DeleteImageFile(@"/pic/", ps.ProPic);
                }
            }
            if (products.Count <= 0)
            {
                return -1;
            }
            return ProductRepository.Delete(products);
        }
        public int UpCategory(Category cat)
        {
            return CategoryRepository.Update(cat);
        }
        public int DeleteCategory(int index)
        {
            return CategoryRepository.Delete(index);
        }
        public int DeleteCategory(int[] list)
        {
            List<Category> categorys = new List<Category>();
            foreach (var item in list)
            {
                Category cs = CategoryRepository.Entities.SingleOrDefault(c => c.Id == item);
                if (cs != null)
                {
                    categorys.Add(cs);
                    //MvcUploader.DeleteImageFile(@"/pic/", ps.ProPic);
                }
            }
            if (categorys.Count <= 0)
            {
                return -1;
            }
            return CategoryRepository.Delete(categorys);
        }


        public int UpProduct(Product product)
        {
            return ProductRepository.Update(product);
        }
        public int UpProduct(IEnumerable<Product> products)
        {
            return ProductRepository.Update(products);
        }


        public int DeleteProduct(IEnumerable<Product> products)
        {
            return ProductRepository.Delete(products);
        }

        public int DeleteCategory(Category category)
        {
            return CategoryRepository.Delete(category);
        }
    }
}
