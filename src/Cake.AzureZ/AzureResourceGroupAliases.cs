using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.AzureZ
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
        public static bool AzureResourceGroupExists(this ICakeContext context,
                                                    Credentials credentials,
                                                    string subscriptionId,
                                                    string resourceGroupName)
        {
            return AzureResourceGroupService.AzureResourceGroupExists(credentials,
                                                                      subscriptionId,
                                                                      resourceGroupName);
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
        public static void EnsureAzureResourceGroupExists(this ICakeContext context,
                                                          Credentials credentials,
                                                          string subscriptionId,
                                                          string resourceGroupName,
                                                          string resourceGroupLocation)
        {
            AzureResourceGroupService.EnsureAzureResourceGroupExists(context.Log,
                                                                     credentials,
                                                                     subscriptionId,
                                                                     resourceGroupName,
                                                                     resourceGroupLocation);
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
        public static void DeleteAzureResourceGroup(this ICakeContext context,
                                                    Credentials credentials,
                                                    string subscriptionId,
                                                    string resourceGroupName)
        {
            AzureResourceGroupService.DeleteAzureResourceGroup(context.Log,
                                                               credentials,
                                                               subscriptionId,
                                                               resourceGroupName);
        }

        /// <summary>
        /// Deploys resources to the resource group using the specified ARM template.
        /// </summary>
        /// <param name="context">The Cake context.</param>
        /// <param name="credentials">The Azure credentials.</param>
        /// <param name="subscriptionId">The subscription ID.</param>
        /// <param name="resourceGroupName">The resource group name.</param>
        /// <param name="deploymentName">The deployment name.</param>
        /// <param name="template">The content of the ARM template file.</param>
        /// <param name="parameters">The content of the ARM template parameters file.</param>
        /// <returns>The outputs from the ARM template deployment.</returns>
        [CakeAliasCategory("ResourceGroup")]
        [CakeMethodAlias]
        public static string DeployAzureResourceGroup(this ICakeContext context,
                                                      Credentials credentials,
                                                      string subscriptionId,
                                                      string resourceGroupName,
                                                      string deploymentName,
                                                      string template,
                                                      string parameters)
        {
            return AzureResourceGroupService.DeployAzureResourceGroup(context.Log,
                                                                      credentials,
                                                                      subscriptionId,
                                                                      resourceGroupName,
                                                                      deploymentName,
                                                                      template,
                                                                      parameters);
        }
    }
}