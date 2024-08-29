namespace ShinyMultiSeed.Calculator.Strategy
{
    public interface ISeedCheckStrategy<TSeed, TResult>
    {
        TResult Check(TSeed initialSeed);
    }
}
