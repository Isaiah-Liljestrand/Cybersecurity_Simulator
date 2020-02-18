using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Disease
{
    public string diseasename;
    public int turnsbeforespread;
    public int currentturn;

    public void Turnpassed()
    {
        currentturn++;
        if (currentturn > turnsbeforespread)
        {
            Spread();
            currentturn = 0;
        }
    }

    public void Spread()
    {

    }
}
