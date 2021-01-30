public interface ISimulatedComponent
{
    void OnRecordSimulationStarted();

    void OnRecordSimulationFinished();

    void OnRunSimulationStarted();

    void OnRunSimulationFinished();

    void Record(float simulationTime);

    void TickSimulation(float simulationTime);
}