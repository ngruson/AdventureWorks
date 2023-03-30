using AutoFixture.Xunit2;
using AW.ConsoleTools.Handlers.AzureAD.AddUserToGroup;
using AW.ConsoleTools.Handlers.AzureAD.CreateUser;
using AW.ConsoleTools.Handlers.AzureAD.CreateUsers;
using AW.ConsoleTools.Handlers.AzureAD.GetGroup;
using AW.ConsoleTools.Handlers.AzureAD.GetUser;
using AW.Services.HumanResources.Core.Handlers.GetAllEmployees;
using AW.SharedKernel.UnitTesting;
using AW.SharedKernel.ValueTypes;
using FluentAssertions;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.ConsoleTools.UnitTests
{
    public class CreateUsersCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_UsersDoNotExists_CreateUsers(
            [Frozen] Mock<IMediator> mockMediator,
            CreateUsersCommandHandler sut,
            CreateUsersCommand command,
            List<NameFactory> names,
            List<List<EmployeeDepartmentHistory>> departmentHistory
        )
        {
            // Arrange
            foreach (var _ in departmentHistory)
            {
                foreach (var dep in _)
                {
                    var groupName = dep.Department?.GroupName;

                    mockMediator.Setup(_ => _.Send(
                            It.Is<GetGroupQuery>(_ => _.GroupName == groupName),
                            It.IsAny<CancellationToken>()
                        )
                    )
                    .ReturnsAsync(new Handlers.AzureAD.GetGroup.Group
                        {
                            DisplayName = groupName
                        }
                    );
                }
            }

            var employees = new List<Employee>
            {
                new Employee(names[0], departmentHistory[0]),
                new Employee(names[1], departmentHistory[1]),
                new Employee(names[2], departmentHistory[2])
            };

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetAllEmployeesQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(employees);

            employees.ForEach(emp =>
            {
                var name = emp.Name?.FullName;
                mockMediator.SetupSequence(_ => _.Send(
                        It.Is<GetUserQuery>(_ => _.UserName == name),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(null)
                .ReturnsAsync(new Handlers.AzureAD.GetUser.User
                    { 
                        DisplayName = emp?.Name?.FullName,
                        MemberOf = new List<Handlers.AzureAD.GetUser.Group>()
                    }
                );
            });

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetAllEmployeesQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetGroupQuery>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Exactly(employees.Count)
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<CreateUserCommand>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Exactly(employees.Count)
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetUserQuery>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Exactly(employees.Count * 2)
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<AddUserToGroupCommand>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Exactly(employees.Count)
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_UsersExistsButNotInGroup_AddUsersToGroup(
            [Frozen] Mock<IMediator> mockMediator,
            CreateUsersCommandHandler sut,
            CreateUsersCommand command,
            List<NameFactory> names,
            List<List<EmployeeDepartmentHistory>> departmentHistory
        )
        {
            // Arrange
            foreach (var _ in departmentHistory)
            {
                foreach (var dep in _)
                {
                    var groupName = dep.Department?.GroupName;

                    mockMediator.Setup(_ => _.Send(
                            It.Is<GetGroupQuery>(_ => _.GroupName == groupName),
                            It.IsAny<CancellationToken>()
                        )
                    )
                    .ReturnsAsync(new Handlers.AzureAD.GetGroup.Group
                    {
                        DisplayName = groupName
                    }
                    );
                }
            }

            var employees = new List<Employee>
            {
                new Employee(names[0], departmentHistory[0]),
                new Employee(names[1], departmentHistory[1]),
                new Employee(names[2], departmentHistory[2])
            };

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetAllEmployeesQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(employees);

            employees.ForEach(emp =>
            {
                var name = emp.Name?.FullName;
                mockMediator.Setup(_ => _.Send(
                        It.Is<GetUserQuery>(_ => _.UserName == name),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(new Handlers.AzureAD.GetUser.User
                {
                    DisplayName = emp?.Name?.FullName,
                    MemberOf = new List<Handlers.AzureAD.GetUser.Group>()
                }
                );
            });

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetAllEmployeesQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetGroupQuery>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Exactly(employees.Count)
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetUserQuery>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Exactly(employees.Count)
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<CreateUserCommand>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<AddUserToGroupCommand>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Exactly(employees.Count)
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_UsersExistsAndInGroup_AddUsersToGroupNotCalled(
            [Frozen] Mock<IMediator> mockMediator,
            CreateUsersCommandHandler sut,
            CreateUsersCommand command,
            List<NameFactory> names,
            List<List<EmployeeDepartmentHistory>> departmentHistory
        )
        {
            // Arrange
            var groups = new List<Handlers.AzureAD.GetGroup.Group>();

            foreach (var _ in departmentHistory)
            {
                foreach (var dep in _)
                {
                    var groupName = dep.Department?.GroupName;
                    var group = new Handlers.AzureAD.GetGroup.Group
                    {
                        Id = Guid.NewGuid().ToString(),
                        DisplayName = groupName
                    };
                    groups.Add(group);

                    mockMediator.Setup(_ => _.Send(
                            It.Is<GetGroupQuery>(_ => _.GroupName == groupName),
                            It.IsAny<CancellationToken>()
                        )
                    )
                    .ReturnsAsync(group);
                }
            }

            var employees = new List<Employee>
            {
                new Employee(names[0], departmentHistory[0]),
                new Employee(names[1], departmentHistory[1]),
                new Employee(names[2], departmentHistory[2])
            };

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetAllEmployeesQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(employees);

            employees.ForEach(emp =>
            {
                var groupName = emp.DepartmentHistory[0].Department?.GroupName;
                var group = groups.Single(_ => _.DisplayName == groupName);

                var name = emp.Name?.FullName;
                mockMediator.Setup(_ => _.Send(
                        It.Is<GetUserQuery>(_ => _.UserName == name),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(new Handlers.AzureAD.GetUser.User
                {
                    DisplayName = emp?.Name?.FullName,
                    MemberOf = new List<Handlers.AzureAD.GetUser.Group>
                    {
                        new Handlers.AzureAD.GetUser.Group(
                             group.Id!,
                             group.DisplayName!
                        )
                    }
                });
            });

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetAllEmployeesQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetGroupQuery>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Exactly(employees.Count)
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetUserQuery>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Exactly(employees.Count)
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<CreateUserCommand>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<AddUserToGroupCommand>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_EmployeesNotFound_ThrowArgumentNullException(
            [Frozen] Mock<IMediator> mockMediator,
            CreateUsersCommandHandler sut,
            CreateUsersCommand command
        )
        {
            // Arrange

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'employees')");

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetAllEmployeesQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetGroupQuery>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetUserQuery>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<CreateUserCommand>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<AddUserToGroupCommand>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_GroupNotFound_ThrowArgumentNullException(
            [Frozen] Mock<IMediator> mockMediator,
            CreateUsersCommandHandler sut,
            CreateUsersCommand command,
            List<NameFactory> names,
            List<List<EmployeeDepartmentHistory>> departmentHistory
        )
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee(names[0], departmentHistory[0]),
                new Employee(names[1], departmentHistory[1]),
                new Employee(names[2], departmentHistory[2])
            };

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetAllEmployeesQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(employees);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'group')");

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetAllEmployeesQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetGroupQuery>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetUserQuery>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<CreateUserCommand>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<AddUserToGroupCommand>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_UserNotFoundAfterCreation_ThrowArgumentNullException(
            [Frozen] Mock<IMediator> mockMediator,
            CreateUsersCommandHandler sut,
            CreateUsersCommand command,
            List<NameFactory> names,
            List<List<EmployeeDepartmentHistory>> departmentHistory
        )
        {
            // Arrange
            foreach (var _ in departmentHistory)
            {
                foreach (var dep in _)
                {
                    var groupName = dep.Department?.GroupName;

                    mockMediator.Setup(_ => _.Send(
                            It.Is<GetGroupQuery>(_ => _.GroupName == groupName),
                            It.IsAny<CancellationToken>()
                        )
                    )
                    .ReturnsAsync(new Handlers.AzureAD.GetGroup.Group
                    {
                        DisplayName = groupName
                    }
                    );
                }
            }

            var employees = new List<Employee>
            {
                new Employee(names[0], departmentHistory[0]),
                new Employee(names[1], departmentHistory[1]),
                new Employee(names[2], departmentHistory[2])
            };

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetAllEmployeesQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(employees);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'user')");

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetAllEmployeesQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetGroupQuery>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetUserQuery>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Exactly(2)
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<CreateUserCommand>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<AddUserToGroupCommand>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }
    }
}
