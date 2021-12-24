using UnityEngine;

public class ProceduralGround : ProceduralBlock
{
   
 
    int rand;
    

    public void SpawnGroundChunk(GROUNDTYPES groundType)
    {
        rand = Random.Range(0, numberOfUnit);
      
        for (int i = 0; i < numberOfUnit; i++)
        {
            if (groundType == GROUNDTYPES.COIN)
            {

             
                if (i == rand)
                {
                    
              
                    Spawnblock(new Vector3(transform.position.x + i, 0, 0), unitCoinGroundPrefab);
                       
                }
                
                else
                {
                    Spawnblock(new Vector3(transform.position.x + i, 0, 0), unitGroundPrefab);
                }
            }
            else
            {
                Spawnblock(new Vector3(transform.position.x + i, 0, 0), unitGroundPrefab);
            }

        }
    }
}
