﻿using Mindblower.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace Mindblower.Core
{
    public class Synchronizer : IAmItRequestListener
    {
        public static bool IsUsed = false;
        private List<GameObject> _levelStarters { get; set; }
        public Synchronizer()
        {
            _levelStarters = GameObject.FindGameObjectsWithTag("Starter").ToList();
            _levelStarters.Sort((a, b) => ExtractNumber(a.name) - ExtractNumber(b.name));
        }

        public void Synchronize()
        {
            IAmItHttpRequest.Get<GetUserInformationModel>(IAmItServerMethods.GET_INFO, this);
            IsUsed = true;
        }
        private int ExtractNumber(string s)
        {
            return Int32.Parse(Regex.Match(s, @"\d+").Value);
        }
        public void OnFail(string code)
        {
            Debug.Log("Something go wrong. Request has returned with code " + code);
        }

        public void OnGet<T>(T responseModel)
        {
            var levels = (responseModel as GetUserInformationModel).Levels;
            levels.Sort((a, b) => ExtractNumber(a.Name) - ExtractNumber(b.Name));
            _levelStarters.ForEach((s) => { s.GetComponentsInChildren<SpriteRenderer>().Where(r => r.name.Contains("star")).ToList().ForEach((y) => { y.enabled = false; }); });
            var enumerator = _levelStarters.GetEnumerator();
            enumerator.MoveNext();
            levels.ForEach((x) => {
                PlayerPrefs.SetInt(x.Name, x.StarsCount);
                enumerator.Current.GetComponentsInChildren<SpriteRenderer>().Where(r => r.name.Contains("star")).Take(x.StarsCount).ToList().ForEach((y) => { y.enabled = true; });
                enumerator.MoveNext();
            });
        }

        public void OnLogin()
        {
            throw new NotImplementedException();
        }

        public void OnLogOut()
        {
            throw new NotImplementedException();
        }

        public void OnPost(string s)
        {
            throw new NotImplementedException();
        }

    }
}