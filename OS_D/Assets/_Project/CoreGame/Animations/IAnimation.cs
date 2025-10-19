namespace CoreGame
{
    public interface IAnimation
    {
        void Play();
    }

    public interface IAnimation<T1>
    {
        void Play(T1 param1);
    }

    public interface IAnimation<T1, T2>
    {
        void Play(T1 param1, T2 param2);
    }

    public interface IAnimation<T1, T2, T3>
    {
        void Play(T1 param1, T2 param2, T3 param3);
    }
}