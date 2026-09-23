using UnityEngine;

public class firstgenerative : MonoBehaviour
{

    public GameObject ground;
    public GameObject ground2;
    Vector3 wallspawn;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {



        Vector3 wallPosi = ground.transform.position;
        //.Faire des grounds qui se genere
        generateGround();

        generateWall();
    }



    private void generateGround()
    {
        for (int i = 0; i < 100; i++)
        {

            //. permettre avec du random d'instancier des grounds avec differents etats (ground, ground2)
            for (int j = 0; j < 100; j++)
            {
                int probaGrnd = Random.Range(0, 10);


                //. si le randomiser renvoie une valeur >5 instancier un ground inon instancier un ground2
                if (probaGrnd > 5)
                {
                    int groundBlank = probaGrnd;
                    GameObject truc = Instantiate(ground, new Vector3(j, 1, i), Quaternion.identity);
                    
                }

                else
                {
                    Instantiate(ground2, new Vector3(j, 1, i), Quaternion.identity);
                }
            }
        }
    }

    private static void generateWall()
    {
        //.  creer des murs si l'etat du ground est = ground2, si le GO est ground2 instancie un mur sinon ne rien faire


        //.creer un tableau contenant tout les GO de type ground
        ground[] grounds = GameObject.FindObjectsByType<ground>();

        //.verifie chaques ground pour chaque ground instancier(10000)
        for (int g = 0; g < 10000; g++) 
        {
            GameObject truc = grounds[g].gameObject;
            Debug.Log(g);

            if (truc.GetComponent<ground>().type == global::ground.typeGround.ROUGE) ;
            {
                Debug.Log("je suis rouge bitch");
                Instantiate
            }
        }



        
    }




    // Update is called once per frame
    void Update()
    {

    }


}
