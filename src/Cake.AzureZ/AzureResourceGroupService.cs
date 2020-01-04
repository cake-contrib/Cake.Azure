using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Cake.Core.Diagnostics;

namespace Cake.AzureZ
{
    public static class AzureResourceGroupService
    {
        public static bool AzureResourceGroupExists(Credentials credentials,
                                                    string subscriptionId,
                                                    string resourceGroupName)
        {
            var client = GetClient(credentials, subscriptionId);

            return client.ResourceGroups.CheckExistence(resourceGroupName);
        }

        public static void EnsureAzureResourceGroupExists(ICakeLog log,
                                                          Credentials credentials,
                                                          string subscriptionId,
                                                          string resourceGroupName,
                                                          string resourceGroupLocation)

        {
            var client = GetClient(credentials, subscriptionId);

            if (client.ResourceGroups.CheckExistence(resourceGroupName) != true)
            {
                log.Information($"Creating resource group '{resourceGroupName}' in location '{resourceGroupLocation}'");
                var resourceGroup = new ResourceGroup
                {
                    Location = resourceGroupLocation
                };
                client.ResourceGroups.CreateOrUpdate(resourceGroupName, resourceGroup);
            }
            else
            {
                log.Information($"Resource group '{resourceGroupName}' already exists.");
            }
        }

        public static void DeleteAzureResourceGroup(ICakeLog log,
                                                    Credentials credentials,
                                                    string subscriptionId,
                                                    string resourceGroupName)
        {
            var client = GetClient(credentials, subscriptionId);

            if (client.ResourceGroups.CheckExistence(resourceGroupName))
            {
                log.Information($"Deleting resource group '{resourceGroupName}'");

                client.ResourceGroups.Delete(resourceGroupName);
            }
            else
            {
                log.Information($"Resource group '{resourceGroupName}' doesn't exist.");
            }
        }

        public static string DeployAzureResourceGroup(ICakeLog log,
                                                      Credentials credentials,
                                                      string subscriptionId,
                                                      string resourceGroupName,
                                                      string deploymentName,
                                                      string template,
                                                      string parameters)
        {
            var client = GetClient(credentials, subscriptionId);

            log.Information($"Starting template deployment '{deploymentName}' in resource group '{resourceGroupName}'");
            dynamic parametersObject = JsonConvert.DeserializeObject(parameters);
            var deployment = new Deployment
            {
                Properties = new DeploymentProperties
                {
                    Mode = DeploymentMode.Incremental,
                    Template = JsonConvert.DeserializeObject(template),
                    Parameters = parametersObject["parameters"].ToObject<JObject>()
                }
            };

            var deploymentResult = client.Deployments.CreateOrUpdate(resourceGroupName,
                deploymentName, deployment);

            log.Information($"Deployment status: {deploymentResult.Properties.ProvisioningState}");

            return JsonConvert.SerializeObject(deploymentResult.Properties.Outputs);
        }

        private static ResourceManagementClient GetClient(Credentials credentials,
                                                          string subscriptionId)
        {
            return new ResourceManagementClient(credentials.ServiceClientCredentials)
            {
                SubscriptionId = subscriptionId
            };
        }
    }
}