namespace Level52TheFinalBattle.Records;

public record ConsoleTypeCheckOutput<T>(bool Success, T ParsedValue, string ErrorMessage);
