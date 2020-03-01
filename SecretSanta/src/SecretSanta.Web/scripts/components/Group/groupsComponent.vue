<template>
    <p>
        <a asp-action="Create">Create New</a>
    </p>
    <table class="table">
        <thead>
            <tr>
                <th asp-display-name-for="ToList()[0].Id"></th>
                <th asp-display-name-for="ToList()[0].Title"></th>
                <th></th>
            </tr>
        </thead>
        <tbody>
            @foreach (var item in Model)
            {
            <tr>
                <td asp-display-for="@item.Id"></td>
                <td asp-display-for="@item.Title"></td>
                <td>
                    <a asp-action="Edit" asp-route-id="@item.Id">Edit</a> |
                    <a asp-action="Delete" asp-route-id="@item.Id">Delete</a> |
                    <a asp-action="Details" asp-route-id="@item.Id">Details</a>
                </td>
            </tr>
            }
        </tbody>
    </table>
</template>
<script lang="ts">

</script>