using System;

public class TestMethodAttribute : Attribute
{
    public bool isEnableNotPlaying;
    public TestMethodAttribute(bool isEnableNotPlaying = true)
    {
        this.isEnableNotPlaying = isEnableNotPlaying;
    }
}
