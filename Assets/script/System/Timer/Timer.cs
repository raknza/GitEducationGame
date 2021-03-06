using System;


public class Timer
{
    public float duration { get; private set; } // 秒數
    public object data { get; private set; } // 參數
    public bool finished { get; private set; } 
    public Type type{get; private set;}
    private Action<object> finished_event;// 秒數歸零後的觸發事件

    public enum Type
    {
        countdown,
        summer
    }



    public Timer(float duration)
    {
        this.duration = duration;
        type = Type.summer;
    }


    public Timer(float duration,Action<object> onTimerFinished = null, object data = null)
    {
        this.duration = duration;
        finished_event = onTimerFinished;
        finished = false;
        this.data = data;
        type = Type.countdown;
    }


    public void Update(float deltaTime)
    {
        if (type == Type.countdown)
        {
            duration -= deltaTime;

            if (duration <= 0 && !finished)
            {
                finished = true;

                if (null != finished_event)
                {
                    finished_event(data);
                }
            }
        }
        else
        {
            duration += deltaTime;
        }
    }
}