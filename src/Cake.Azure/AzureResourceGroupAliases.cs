using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.Diagnostics;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Models;
using Microsoft.Rest;

namespace Cake.Azure
{
    /// <summary>
    /// Contains Cake aliases for running commands managing Azure resource groups.
    /// </summary>
    [CakeAliasCategory("Azure")]
    public static class AzureResourceGroupAliases
    {
        /// <summary>
        /// Checks whether a resource group with the specified name exists.
        /// </summary>
        /// <param name="context">The Cake context.</param>
        /// <param name="credentials">The Azure credentials.</param>
        /// <param name="subscriptionId">The subscription ID.</param>
        /// <param name="resourceGroupName">The resource group name.</param>
        /// <returns><code>true</code>, if a resource group exists; <code>false</code>, otherwise.</returns>
        [CakeAliasCategory("ResourceGroup")]
        [CakeMethodAlias]
        public static bool AzureResourceGroupExists(this ICakeContext context, ServiceClientCredentials credentials,
            string subscriptionId, string resourceGroupName)
        {
            var client = GetClient(credentials, subscriptionId);

            return client.ResourceGroups.CheckExistence(resourceGroupName);
        }

        /// <summary>
        /// Creates a resource group if it doesn't exist.
        /// </summary>
        /// <param name="context">The Cake context.</param>
        /// <param name="credentials">The Azure credentials.</param>
        /// <param name="subscriptionId">The subscription ID.</param>
        /// <param name="resourceGroupName">The resource group name.</param>
        /// <param name="resourceGroupLocation">The resource group location.</param>
        [CakeAliasCategory("ResourceGroup")]
        [CakeMethodAlias]
        public static void EnsureResourceGroupExists(this ICakeContext context, ServiceClientCredentials credentials,
            string subscriptionId, string resourceGroupName, string resourceGroupLocation)
        {
            var client = GetClient(credentials, subscriptionId);

            if (client.ResourceGroups.CheckExistence(resourceGroupName) != true)
            {
                context.Log.Information($"Creating resource group '{resourceGroupName}' in location '{resourceGroupLocation}'");
                var resourceGroup = new ResourceGroup
                {
                    Location = resourceGroupLocation
                };
                client.ResourceGroups.CreateOrUpdate(resourceGroupName, resourceGroup);
            }
            else
            {
                context.Log.Information($"Resource group '{resourceGroupName}' already exists.");
            }
        }

        /// <summary>
        /// Delete the specified resource group.
        /// </summary>
        /// <param name="context">The Cake context.</param>
        /// <param name="credentials">The Azure credentials.</param>
        /// <param name="subscriptionId">The subscription ID.</param>
        /// <param name="resourceGroupName">The resource group name.</param>
        [CakeAliasCategory("ResourceGroup")]
        [CakeMethodAlias]
        public static void DeleteAzureResouceGroup(this ICakeContext context, ServiceClientCredentials credentials,
            string subscriptionId, string resourceGroupName)
        {
            var client = GetClient(credentials, subscriptionId);

            if (client.ResourceGroups.CheckExistence(resourceGroupName))
            {
                context.Log.Information($"Deleting resource group '{resourceGroupName}'");

                client.ResourceGroups.Delete(resourceGroupName);
            }
            else
            {
                context.Log.Information($"Resource group '{resourceGroupName}' doesn't exist.");
            }
        }

        private static ResourceManagementClient GetClient(ServiceClientCredentials credentials, string subscriptionId)
        {
            return new ResourceManagementClient(credentials)
            {
                SubscriptionId = subscriptionId
            };
        }
    }
}