// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let apiurl = "https://forkify-api.herokuapp.com/api/v2/recipes";
let apikey = "0e3c309b-d6db-4572-be1c-a31c5f06decd";
async function GetRecipes(Recipename , id ,isallshow) {
    let response = await fetch(`${apiurl}?search=${Recipename}&key=${apikey}`);
    let result = await response.json();
    let Recpice = isallshow ? result.data.recipes : result.data.recipes.slice(1, 7);
    ShowRecipes(Recpice, id);
}

function ShowRecipes(recipes, id) {
    $.ajax({
        contentType : "application/json; charset=utf-8;",
        dataType: 'html',
        type: 'POST',
        url: '/Recipe/GetRecipeCard',
        data: JSON.stringify(recipes),
        success: function (htmlResult) {
            $('#' + id).html(htmlResult);
        }

    }
    )
}
async function GetOrderRecipe(id,showId) {
    let response = await fetch(`${apiurl}/${id}?key=${apikey}`);
    let result = await response.json();
    console.log(result);
    let recipe = result.data.recipe;
    ShowOrderRecipeDetails(recipe, showId);
}
function ShowOrderRecipeDetails(orderRecipeDetails, showId) {
    $.ajax({
        url: '/Recipe/ShowOrder',
        data: orderRecipeDetails,
        dataType: 'html',
        type: 'POST', // Use 'type' instead of 'Type'

        success: function (htmlResult) {
            $('#' + showId).html(htmlResult);

        }
    })
}