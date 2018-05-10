using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Core.DataAccess;
using Core.DTOs;
using Core.Entities;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Core.Tests.ThreadEntryReactions
{
    public class ThreadEntryReactionTests
    {
        private readonly Mock<AppDbContext> contextMock;
        private readonly Mock<DbSet<ThreadEntryReaction>> setMock;
        private readonly ThreadEntryService threadEntryService;
        private readonly IQueryable<ThreadEntryReaction> reactions;

        private readonly string EXISTING_REACTION_TAG = ":existing_tag:";
        private readonly long THREAD_ENTRY_ID = 1;

        public ThreadEntryReactionTests()
        {

            reactions = new List<ThreadEntryReaction>()
            {
                new ThreadEntryReaction()
                {
                    Id = 1,
                    ReactionTag = ":new_reaction:",
                    ThreadEntryId = THREAD_ENTRY_ID
                }
            }.AsQueryable();

            var mapperMock = new Mock<IMapper>();
            setMock = DbContextQueryableHelper<ThreadEntryReaction>.GetMockedDbSet(reactions);
            mapperMock.Setup(m => m.Map<ThreadEntryReaction>(It.IsAny<AddEntryReactionDto>())).Returns(new ThreadEntryReaction());

            contextMock = new Mock<AppDbContext>();
            contextMock.Setup(m => m.ThreadEntryReactions).Returns(setMock.Object);
            threadEntryService = new ThreadEntryService(contextMock.Object, mapperMock.Object);

        }

        [Fact]
        public void ReactionDoesNotExist_ReactionIsAdded_ItIsPersisted()
        {
            var addReactionDto = new AddEntryReactionDto()
            {
                ReactionTag = EXISTING_REACTION_TAG,
                ThreadEntryId = THREAD_ENTRY_ID
            };

            threadEntryService.AddReactionToThreadEntry(addReactionDto, string.Empty);

            setMock.Verify(m => m.Add(It.IsAny<ThreadEntryReaction>()), Times.Once);
            contextMock.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public void ReactionExists_ReactionIsAdded_ItIsNotPersisted()
        {
            var addReactionDto = new AddEntryReactionDto()
            {
                ReactionTag = ":new_reaction:",
                ThreadEntryId = 1
            };

            threadEntryService.AddReactionToThreadEntry(addReactionDto, string.Empty);

            setMock.Verify(m => m.Add(It.IsAny<ThreadEntryReaction>()), Times.Never);
            contextMock.Verify(m => m.SaveChanges(), Times.Never);
        }
    }
}
