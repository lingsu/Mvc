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
    /// 仓储操作实现——用户信息
    /// </summary>
    [Export(typeof(IMemberRepository))]
    public class MemberRepository:EFRepositoryBase<Member>,IMemberRepository
    {
    }
}
