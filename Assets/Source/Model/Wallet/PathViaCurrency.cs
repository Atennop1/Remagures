using SaveSystem.Paths;
using UnityEngine;

namespace Remagures.Model.Wallet
{
    public sealed class PathViaCurrency : IPath
    {
        public string Name { get; }

        public PathViaCurrency(Currency currency) 
            => Name = System.IO.Path.Combine(Application.persistentDataPath, currency.ToString());
    }
}