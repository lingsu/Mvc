using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

using Component.Data;
using Core.Models;
using Core.Data.Repositories;

namespace Core.Data.Repositories.Impl
{
    /// <summary>
    /// 仓储操作实现——登录记录信息
    /// </summary>
    [Export(typeof(ILoginLogRepository))]
    public class LoginLogRepository:EFRepositoryBase<LoginLog>,ILoginLogRepository
    {
    }
}
