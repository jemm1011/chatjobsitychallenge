using AutoMapper;
using ChatJobsity.Chat.Domain.Services;
using ChatJobsity.Chat.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using ChatJobsity.Chat.Controllers;
using ChatJobsity.Chat.Domain.Repositories;
using ChatJobsity.Chat.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ChatJobsity.Chat.Profiles;

namespace ChatJobsity.Chat.Tests
{
    [TestClass]
    public class TestRoomController
    {

        private readonly RoomController _roomController;

        private Mock<IUnitOfWork>_mockUnitOfWork;
        private Mock<IRoomRepository> _mockRepository;
        private IMapper _mapper;

        public TestRoomController()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new RoomProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _mockRepository = new Mock<IRoomRepository>();

            _mockRepository.Setup(x => x.GetOwnRooms(It.IsAny<Guid>())).ReturnsAsync(MockRooms);

            _mockUnitOfWork.Setup(x => x.Rooms).Returns(_mockRepository.Object);

            _roomController = new RoomController(_mockUnitOfWork.Object, _mapper);
        }

        [TestMethod]
        public async Task GetOwnRooms_ShouldReturnAllRooms()
        {
            var test = _mockRepository.Object.GetOwnRooms(It.IsAny<Guid>());

            var ownRooms = await _roomController.GetOwnRooms(It.IsAny<Guid>());
            Assert.IsNotNull(ownRooms);
            Assert.IsTrue(ownRooms?.Count > 0);
        }

        private List<Room> MockRooms {
            get 
            {
                return new List<Room>()
                {
                    new Room
                    {
                        Id = It.IsAny<Guid>(),
                        Participants = MockRoomParticipants,
                        Messages = new List<Message>()
                    },
                    new Room
                    {
                        Id = It.IsAny<Guid>(),
                        Participants = MockRoomParticipants,
                        Messages = new List<Message>()
                    }
                };
            }
        }

        private List<RoomParticipant> MockRoomParticipants
        {
            get
            {
                return new List<RoomParticipant>()
                {
                    new RoomParticipant
                    {
                        Id = It.IsAny<Guid>(),
                        Room = new Room {
                           Id = It.IsAny<Guid>(),
                           Messages = new List<Message>(),
                           LastUpdatedDateTime = DateTime.Now
                        }
                    },
                   new RoomParticipant
                    {
                        Id = It.IsAny<Guid>(),
                        Room = new Room {
                           Id = It.IsAny<Guid>(),
                           Messages = new List<Message>(),
                           LastUpdatedDateTime = DateTime.Now
                        }
                    },
                };
            }
        }
    }
}
