using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapSelectScript : UIFader
{
    public override void FadeIn()
    {
        base.FadeIn();
        var widgets = transform.GetComponentsInChildren<BGChgBtn>(includeInactive: true)
                            .Where(w => w.gameObject != transform.gameObject); // 부모 자신 제외(원하면)

        foreach (var w in widgets)
            w.Refresh();
    }
}
