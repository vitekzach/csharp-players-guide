using Level31TheFountainOfObjects.Enums;

namespace Level31TheFountainOfObjects.Interfaces;

public interface IOutputInterface
{
    void Output(string output, OutputType outputType, bool writeLine=true);
}