using UnityEngine;
using System.Collections;

public class FunFactDataHandler : MonoBehaviour
{
    FunFactEntity _entity;
    FunFactRepository _repository;

    private void Start()
    {
        _repository = new FunFactRepository();
    }

    public void CreateAllFunFacts()
    {
        //Dummie fun facts.
        CreateFunFact("All achievement names are actually parodies of song titles.");
        CreateFunFact("Xevy is actually a very sensitive guy.");
        CreateFunFact("The Game. You lost it.");
        CreateFunFact("No skeltals were hurt in the making of this game.");
        CreateFunFact("Spam Games' original name was Pawn V.");

    }


    private void CreateFunFact(string description)
    {
        FunFactEntity entity = new FunFactEntity();
        entity.Description = description;
        _repository.Add(entity);
    }
}
