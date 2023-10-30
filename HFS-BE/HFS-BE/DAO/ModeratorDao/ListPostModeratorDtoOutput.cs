using HFS_BE.Base;
using HFS_BE.DAO.CustomerDao;

namespace HFS_BE.DAO.ModeratorDao
{
	public class ListPostModeratorDtoOutput: BaseOutputDto
	{
		public List<PostModeratorDtoOutput> data { get; set; }
	}
}
