using BlockBase.BBLinq.Enumerables;

namespace BlockBase.BBLinq.Sets.Interfaces
{
    public interface IJoin<TA>
    {
        public IJoin<TA, TB> Join<TB>(BlockBaseJoinEnum type);
    }
    public interface IJoin<TA, TB> : IQueryableJoinSet<TA, TB>
    {
        public IJoin<TA, TB, TC> Join<TC>(BlockBaseJoinEnum type);
    }
    public interface IJoin<TA, TB, TC> : IQueryableJoinSet<TA, TB, TC>
    {
        public IJoin<TA, TB, TC, TD> Join<TD>(BlockBaseJoinEnum type);
    }
    public interface IJoin<TA, TB, TC, TD> : IQueryableJoinSet<TA, TB, TC, TD>
    {
        public IJoin<TA, TB, TC, TD, TE> Join<TE>(BlockBaseJoinEnum type);
    }
    public interface IJoin<TA, TB, TC, TD, TE> : IQueryableJoinSet<TA, TB, TC, TD, TE>
    {
        public IJoin<TA, TB, TC, TD, TE, TF> Join<TF>(BlockBaseJoinEnum type);
    }
    public interface IJoin<TA, TB, TC, TD, TE, TF> : IQueryableJoinSet<TA, TB, TC, TD, TE, TF>
    {
        public IJoin<TA, TB, TC, TD, TE, TF, TG> Join<TG>(BlockBaseJoinEnum type);
    }
    public interface IJoin<TA, TB, TC, TD, TE, TF, TG> : IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG>
    {
        public IJoin<TA, TB, TC, TD, TE, TF, TG, TH> Join<TH>(BlockBaseJoinEnum type);
    }
    public interface IJoin<TA, TB, TC, TD, TE, TF, TG, TH> : IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH>
    {
        public IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI> Join<TI>(BlockBaseJoinEnum type);
    }
    public interface IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI> : IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI>
    {
        public IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ> Join<TJ>(BlockBaseJoinEnum type);
    }
    public interface IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ> : IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ>
    {
        public IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK> Join<TK>(BlockBaseJoinEnum type);
    }
    public interface IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK> : IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK>
    {
        public IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL> Join<TL>(BlockBaseJoinEnum type);
    }
    public interface IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL> : IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL>
    {
        public IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM> Join<TM>(BlockBaseJoinEnum type);
    }
    public interface IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM> : IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM>
    {
        public IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN> Join<TN>(BlockBaseJoinEnum type);
    }
    public interface IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN> : IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN>
    {
        public IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO> Join<TO>(BlockBaseJoinEnum type);
    }

    public interface IJoin<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO> : IQueryableJoinSet<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM, TN, TO>
    {
    }
}
