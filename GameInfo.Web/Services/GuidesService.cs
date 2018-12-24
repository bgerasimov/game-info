using GameInfo.Data;
using GameInfo.Models;
using GameInfo.Models.InputModels;
using GameInfo.Models.ViewModels;
using GameInfo.Services.Contracts;
using Microsoft.AspNetCore.Identity;
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

        public void Add(AddGuideInputModel model, GameInfoUser currentUser)
        {
            var guide = new Guide
            {
                Title = model.GuideTitle,
                Content = model.GuideContent
            };            
            guide.Creator = currentUser;

            this._db.Guides.Add(guide);
            this._db.SaveChanges();
        }

        public IList<GuidesAllViewModel> All()
        {
            var guides = this._db.Guides?
                .Select(x => new Guide
                {
                    Id = x.Id,
                    Content = x.Content,
                    Title = x.Title,
                    Creator = _db.Users.FirstOrDefault(u => u.Id == x.CreatorId),
                    CreatorId = x.CreatorId
                })
                .ToList();

            if (guides != null)
            {
                var guidesModel = guides
                .Select(g => new GuidesAllViewModel
                {
                    Id = g.Id,
                    UserName = g.Creator?.UserName,
                    UserAvatar = g.Creator?.AvatarUrl,
                    GuideTitle = g.Title,
                    ShortContent = g.Content.Substring(0, Math.Min(g.Content.Length, 50))
                }).ToList();

                return guidesModel;
            }
            else
            {
                return null;
            }            
        }

        public Guide ById(int id)
        {
            var guide = _db.Guides.FirstOrDefault(x => x.Id == id);
            if (guide != null)
            {
                guide.Creator = _db.Users.FirstOrDefault(x => x.Id == guide.CreatorId);
            }            

            return guide;
        }

        public bool Delete(int id)
        {
            var guide = this.ById(id);

            if (guide != null)
            {
                _db.Guides.Remove(guide);
                _db.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
