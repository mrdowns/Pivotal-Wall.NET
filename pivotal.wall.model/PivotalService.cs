﻿using System.Collections.Generic;

namespace pivotal.wall.model
{
    public class PivotalService
    {
        private readonly IProjectRepository _projectRepository;

        public PivotalService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public virtual Project GetProject(int id)
        {
            Project project = _projectRepository.GetProject(id);

            IEnumerable<Story> stories = _projectRepository.GetStoriesForProject(id);

            project.Stories = stories;
            
            return project;
        }
    }
}