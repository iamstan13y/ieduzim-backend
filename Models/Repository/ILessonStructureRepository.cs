using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public interface ILessonStructureRepository
    {
        Task<Paginator<LessonStructure>> GetPagedByUserId(PageRequest request, int teacherId);
    }
}