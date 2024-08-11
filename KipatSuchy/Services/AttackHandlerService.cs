using System.Collections.Concurrent;
using KipatSuchy.Data;
using KipatSuchy.Models;

namespace KipatSuchy.Services
{
    public class AttackHandlerService
    {
        public async Task RegisterAndRunAttackTask(Threat t)
        {
            int timeLeft = HelperData.Origins[t.Origin] / HelperData.Veapons[t.Weapon]; 
            for (int i = timeLeft;  i > 0; --i) { 
                Console.WriteLine($"nushar {i} shniot");
                await Task.Delay(1000);
            }
        }
        
    }
}
