using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

using Core.Data.Context;

namespace Core.Data.Initialize
{
    public static class DatabaseInitializer
    {
        public static void Initialize()
        {
            Database.SetInitializer(new SampleData());
            using (var db = new DemoDbContext())
            {
                db.Database.Initialize(false);
            }
        }
    }
}
