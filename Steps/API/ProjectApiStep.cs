using Allure.NUnit.Attributes;
using RestSharp;
using System.Text.Json;
using TestRail.BaseEntities;
using TestRail.Models;
using TestRail.Services.API;

namespace TestRail.Steps.API
{
    public class ProjectApiStep : BaseApiStep
    {
        public ProjectApiStep(RestClient client, ApiService apiServices) : base(client, apiServices)
        {
        }

        private List<ProjectModel> GetProjectList()
        {
            const string endPoint = "index.php?/api/v2/get_projects";

            var response = apiService.CreateGetRequest(client, endPoint);
            var responseContent = apiService.GetContent(response);
            var projectsResponse = JsonSerializer.Deserialize<ProjectListResponse>(responseContent);

            if (projectsResponse == null)
            {
                string errorMessage = "Deserialized project list response is null.";
                logger.Error(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }

            if (projectsResponse.Projects == null)
            {
                string errorMessage = "Projects in projectsResponse is null.";
                logger.Error(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }

            return projectsResponse.Projects.ToList();
        }

        public bool IsProjectInList(int id)
        {
            List<ProjectModel> projects = GetProjectList();
            return projects.Any(project => project.Id == id);
        }

        public bool IsProjectInList(string name)
        {
            List<ProjectModel> projects = GetProjectList();
            return projects.Any(project => project.Name == name);
        }

        public ProjectModel GetProjectByItsName(string name)
        {
            List<ProjectModel> projects = GetProjectList();
            ProjectModel? project = projects.SingleOrDefault(project => project.Name == name);

            if (project == null)
            {
                string errorMessage = $"Project with name '{name}' was not found.";
                logger.Error(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }
            return project;
        }

        public List<ProjectModel> GetProjectsListByPartialProjectName(string projectName)
        {
            List<ProjectModel> projects = GetProjectList();
            List<ProjectModel> selectedProjects =
                        projects
                        .Where(project => project.Name.Contains(projectName))
                        .ToList();
            return selectedProjects;
        }

        [AllureStep("Add a project with a given project model by API and return response")]
        public RestResponse AddProject(ProjectModel projectToCreate)
        {
            const string endPoint = "/index.php?/api/v2/add_project";

            var response = apiService.CreatePostRequest(client, endPoint, JsonSerializer.Serialize(projectToCreate));
            logger.Info($"Added a project: {response.Content}. StatusCode." + response.StatusCode);
            return response;
        }

        [AllureStep("Add a project with a given project model by API and return added project")]
        public ProjectModel AddProjectAndReturnIt(ProjectModel projectToCreate)
        {
            var response = AddProject(projectToCreate);
            ProjectModel createdProject = GetProjectModelFromResponse(response);
            return createdProject;
        }

        [AllureStep("Delete a given project by API and return response")]
        public RestResponse DeleteProject(ProjectModel projectToDelete)
        {
            int project_id = projectToDelete.GetId();
            string endPoint = $"index.php?/api/v2/delete_project/{project_id}";

            var response = apiService.CreatePostRequest(client, endPoint, "Content-Type", "application/json");

            logger.Info($"Deleted a project with id = {project_id}. StatusCode." + response.StatusCode);

            return response;
        }

        [AllureStep("Update a given project by API and return response")]
        public RestResponse UpdateProject(ProjectModel projectToUpdate, ProjectModel newProjectInfo)
        {
            int project_id = projectToUpdate.GetId();
            string endPoint = $"index.php?/api/v2/update_project/{project_id}";

            var response = apiService.CreatePostRequest(client, endPoint, JsonSerializer.Serialize(newProjectInfo));

            logger.Info($"Updated a project: {response.Content}. StatusCode." + response.StatusCode);

            return response;
        }

        public ProjectModel GetProjectModelFromResponse(RestResponse response)
        {
            var projectModel = JsonSerializer.Deserialize<ProjectModel>(apiService.GetContent(response));

            if (projectModel == null)
            {
                string errorMessage = "Deserialized project model is null.";
                logger.Error(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }

            return projectModel;
        }
    }
}
