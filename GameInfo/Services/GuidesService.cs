using GameInfo.Data;
using GameInfo.Models;
using GameInfo.Models.ViewModels;
using GameInfo.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Services
{
    public class GuidesService : IGuidesService
    {
        private readonly GameInfoContext _db;

        public GuidesService(GameInfoContext db)
        {
            _db = db;
        }

        public IList<GuidesAllViewModel> All()
        {
            var guides = _db.Guides.ToList();

            var guidesModel = guides
                .Select(g => new GuidesAllViewModel
                {
                    UserName = g.Creator.UserName,
                    UserAvatar = g.Creator.AvatarUrl,
                    GuideTitle = g.Title,
                    ShortContent = g.Content.Substring(0, 50)
                }).ToList();

            return guidesModel;
        }
    }
}
