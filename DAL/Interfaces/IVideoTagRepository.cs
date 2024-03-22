using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IVideoTagRepository
    {
        IEnumerable<VideoTag> GetVideoTags();
        VideoTag GetVideoTagById(int id);
        void AddVideoTag(VideoTag videoTag);
        void UpdateVideoTag(VideoTag videoTag);
        void DeleteVideoTag(int id);

        void AddTagsToVideo(int videoId, IEnumerable<int> tagIds);
    }
}
