using System.Collections.Generic;
using Com.Game.Scopes.Contracts;
using UnityEngine;

namespace Com.Game.Application.Scopes
{
    public class ApplicationScope : DiScope
    {
        protected override List<string> AssetsToPreload => new()
        {

        };
        
        protected override void OnInitialized()
        {
            Debug.Log("ApplicationScope.OnInitialized");
        }

        protected override void OnDisposed()
        {
            Debug.Log("ApplicationScope.OnDisposed");
        }
    }
}
