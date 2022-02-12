﻿// Create a module service and register a handler to observe the error occurred event.
var moduleService = new ModuleService();
moduleService.ErrorOccurred += RecordMessage;

// Discover any defined modules.
moduleService.Discover();

do {

    // Prompt the user for a module.
    Console.Write("Please enter `help` for help, `exit` to exit, or a command key: ");

    // Store the user's response.
    var response = Console.ReadLine();

    // Determine if we should exit.
    if (response?.Equals("exit", ignoreCase: true) == true)
        break;

    // Determine if help was requested.
    else if (response?.Equals("help", ignoreCase: true) == true)
        Console.WriteLine(moduleService.GenerateHelpMessage());

    // Attempt to invoke a module.
    if (!string.IsNullOrWhiteSpace(response)) {
        if (moduleService.ContainsModule(response, out ConsumableModule? module)) {
            if (module != null) {
                module.MessageReady += RecordMessage;
                moduleService.Invoke(module);
            }
        }
        else
            Console.WriteLine($"No module found with the key `{response}`.");
    } else
        Console.WriteLine("User response lost.");

} while (true);


#region Local Functions

/// <summary>
/// Records a message received from a module or the module service.
/// </summary>
/// <param name="sender">The sender of the event.</param>
/// <param name="message">The message that should be recorded.</param>
void RecordMessage(object? sender, string message) {
    if (sender is ConsumableModule module)
        Console.WriteLine($"{module.Name}: {message}");
    else
        Console.WriteLine(message);
}

#endregion