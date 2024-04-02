using dating_backend.DTOs;
using dating_backend.Entities;
using dating_backend.Extensions;
using dating_backend.Helpers;
using dating_backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dating_backend.Data
{
    public class LikesRepository : ILikesRepository
    {
        private readonly DataContext _context;
        public LikesRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<UserLike> GetUserLike(int sourceUserId, int targetUserId)
        {
            return await _context.Likes.FindAsync(sourceUserId, targetUserId);
        }

        public async Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams)
        {
            // Initialize a queryable list of users, ordered by username. 
            // The AsQueryable method indicates the query is not executed against the database yet.
            var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
            var likes = _context.Likes.AsQueryable();

            // If the predicate is "liked", filter for likes where the current user is the source.
            if (likesParams.Predicate == "liked")
            {
                // Filter the likes to only include those made by the current user.
                likes = likes.Where(like => like.SourceUserId == likesParams.UserId);
                // Select the target users of these likes to form the list of users who were liked by the current user.
                users = likes.Select(like => like.TargetUser);
            }

            // If the predicate is "likedBy", filter for likes where the current user is the target.
            if (likesParams.Predicate == "likedBy")
            {
                // Filter the likes to only include those where the current user is the target.
                likes = likes.Where(like => like.TargetUserId == likesParams.UserId);
                // Select the source users of these likes to form the list of users who liked the current user.
                users = likes.Select(like => like.SourceUser);
            }

            // Project the filtered list of users into a collection of LikeDto objects.
            var likedUsers = users.Select(user => new LikeDto
            {
                Username = user.UserName,
                KnownAs = user.KnownAs,
                Age = user.DateOfBirth.CalculateAge(),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain).Url,
                City = user.City,
                Id = user.Id,
            });

            return await PagedList<LikeDto>.CreateAsync(likedUsers, likesParams.PageNumber, likesParams.PageSize);
        }


        public async Task<User> GetUserWithLikes(int userId)
        {
            return await _context.Users
                .Include(x => x.LikedUsers)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}
