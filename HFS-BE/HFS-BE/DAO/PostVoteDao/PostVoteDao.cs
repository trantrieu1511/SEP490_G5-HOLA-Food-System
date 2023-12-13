using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.DAO.PostVoteDao
{
    public class PostVoteDao : BaseDao
    {
        public PostVoteDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public GetPostVoteDaoOutputDto GetVote(PostVoteDaoInputDto inputDto)
        {
            try
            {
                var vote = this.context.PostVotes
                    .FirstOrDefault(x => x.PostId == inputDto.PostId
                                        && x.VoteBy == inputDto.CustomerId);

                var output = this.Output<GetPostVoteDaoOutputDto>(Constants.ResultCdSuccess);
                if (vote != null)
                {
                    output.CustomerId = vote.VoteBy;
                    output.IsLike = vote.IsLike;
                    output.PostId = vote.PostId;
                }

                return output;
            }
            catch (Exception)
            {
                return this.Output<GetPostVoteDaoOutputDto>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto CreateVote(PostVoteDaoInputDto inputDto)
        {
            try
            {
                var vote = new PostVote()
                {
                    VoteBy = inputDto.CustomerId,
                    IsLike = inputDto.IsLike,
                    CreatedDate = DateTime.Now,
                    PostId = inputDto.PostId,
                };

                this.context.Add(vote);
                this.context.SaveChanges();
                return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto UpdadteVote(PostVoteDaoInputDto inputDto)
        {
            try
            {
                var vote = this.context.PostVotes
                    .FirstOrDefault(x => x.PostId == inputDto.PostId
                                        && x.VoteBy == inputDto.CustomerId);

                if (vote != null)
                {
                    vote.IsLike = inputDto.IsLike;
                    vote.CreatedDate = DateTime.Now;
                    this.context.Update(vote);
                    this.context.SaveChanges();
                }

                return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto DeleteVote(PostVoteDaoInputDto inputDto)
        {
            try
            {
                var vote = this.context.PostVotes.FirstOrDefault(x => x.PostId == inputDto.PostId && x.VoteBy == inputDto.CustomerId);

                if (vote != null)
                {
                    this.context.Remove(vote);
                    this.context.SaveChanges();
                }

                return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
