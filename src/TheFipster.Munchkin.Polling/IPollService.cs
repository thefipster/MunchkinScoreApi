namespace TheFipster.Munchkin.Polling
{
    public interface IPollService<TKey, TValue>
    {
        PollRequest<TValue> StartRequest(TKey requestId);
        bool CheckRequest(TKey requestId);
        void FinishRequest(TKey requestId, TValue requestPayload);
    }
}
