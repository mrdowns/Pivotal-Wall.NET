using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using pivotal.wall.model;
using PivotalTracker.FluentAPI.Domain;
using PivotalTracker.FluentAPI.Repository;

namespace pivotal.wall.repositories
{
    /// <summary>
    /// simply acts as a facade around the fluent api
    /// </summary>
    public class ProjectRepository : IProjectRepository
    {
        private readonly PivotalProjectRepository _pivotalProjectRepository;

        public ProjectRepository(PivotalProjectRepository pivotalProjectRepository)
        {
            Mapper.CreateMap<Project, PivotalProject>();

            _pivotalProjectRepository = pivotalProjectRepository;
        }

        public PivotalProject GetProject(int id)
        {
            return Mapper.Map<Project, PivotalProject>(_pivotalProjectRepository.GetProject(id));
        }
    }
}
