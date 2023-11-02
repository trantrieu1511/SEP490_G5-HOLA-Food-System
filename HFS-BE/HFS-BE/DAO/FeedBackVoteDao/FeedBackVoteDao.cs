using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.DAO.FeedBackVoteDao
{
    public class FeedBackVoteDao : BaseDao
    {
        public FeedBackVoteDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public GetVoteDaoOutputDto GetVote(VoteDaoInputDto inputDto)
        {
            try
            {
                var vote = this.context.FeedbackVotes
                    .FirstOrDefault(x => x.FeedbackId == inputDto.FeedBackId
                                        && x.VoteBy == inputDto.CustomerId);

                var output = this.Output<GetVoteDaoOutputDto>(Constants.ResultCdSuccess);
                if (vote != null)
                {
                    output.CustomerId = vote.VoteBy;
                    output.IsLike = vote.IsLike;
                    output.FeedBackId = vote.FeedbackId;
                }

                return output; 
            }
            catch (Exception)
            {
                return this.Output<GetVoteDaoOutputDto>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto CreateVote(VoteDaoInputDto inputDto)
        {
            try
            {
                var vote = new FeedbackVote()
                {
                    VoteBy = inputDto.CustomerId,
                    IsLike = inputDto.IsLike,
                    CreatedDate = DateTime.Now,
                    FeedbackId = inputDto.FeedBackId,
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

        public BaseOutputDto UpdadteVote(VoteDaoInputDto inputDto)
        {
            try
            {
                var vote = this.context.FeedbackVotes
                    .FirstOrDefault(x => x.FeedbackId == inputDto.FeedBackId
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

        public BaseOutputDto DeleteVote(VoteDaoInputDto inputDto)
        {
            try
            {
                var vote = this.context.FeedbackVotes
                    .FirstOrDefault(x => x.FeedbackId == inputDto.FeedBackId
                                        && x.VoteBy == inputDto.CustomerId);

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
