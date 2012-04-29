[T4Scaffolding.Scaffolder(Description = "Steven Suings Ajax Powered Grid")][CmdletBinding()]
param(        
    [string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

$outputPath = "ExampleOutput"  # The filename extension will be added based on the template's <#@ Output Extension="..." #> directive
$namespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value

Add-ProjectItemViaTemplate $outputPath -Template Steve.Scaffolders.ajaxGridTemplate `
	-Model @{ Namespace = $namespace; ExampleValue = "Hello, world!" } `
	-SuccessMessage "Added Steve.Scaffolders.ajaxGrid output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force