using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedSkin : MonoBehaviour
{
    [SerializeField] Transform avatarTransform;
    [SerializeField] Transform uFOTransform;
    [SerializeField] Transform beamTransform;

    GameObject currentAvatar;
    GameObject currentUFO;
    GameObject currentBeam;


    GameManager gmInst;
    void Awake()
    {
        gmInst = GameManager.current;
    }

    private void OnEnable()
    {
        GameEvents.current.OnAvatarSkinSelect += ShowAvatarSkin;
        GameEvents.current.OnUFOSkinSelect += ShowUFOSkin;
        GameEvents.current.OnBeamSkinSelect += ShowBeamSkin;
    }

    private void OnDisable()
    {
        GameEvents.current.OnAvatarSkinSelect -= ShowAvatarSkin;
        GameEvents.current.OnUFOSkinSelect -= ShowUFOSkin;
        GameEvents.current.OnBeamSkinSelect -= ShowBeamSkin;
    }
    private void Start()
    {
        ShowAvatarSkin();
        ShowUFOSkin();
        ShowBeamSkin();
    }
    public void ShowAvatarSkin()
    {
        RemoveSkin(avatarTransform);
        currentAvatar = gmInst.avatars[ShopManager.selectedAvatarIndex].MainMenuPrefab;
        
       
        Instantiate(currentAvatar, avatarTransform.position, avatarTransform.localRotation ,  avatarTransform);
        
        
    }

    public void ShowUFOSkin()
    {
        RemoveSkin(uFOTransform);
        currentUFO = gmInst.uFOs[ShopManager.selectedUFOIndex].MainMenuPrefab;
        Instantiate(currentUFO, uFOTransform.position, uFOTransform.localRotation, uFOTransform);
    }
    public void ShowBeamSkin()  
    {
        RemoveSkin(beamTransform);
        currentBeam = gmInst.beams[ShopManager.selectedBeamIndex].MainMenuPrefab;
        Instantiate(currentBeam, beamTransform.position, beamTransform.localRotation, beamTransform);
    }

    private void RemoveSkin(Transform parent)
    {
        if(parent.childCount > 0)
        Destroy(parent.GetChild(0).gameObject);
    }

    
}
