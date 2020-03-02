<template>
    <table class="table">
        <thead>
            <tr>
                <th>Id</th>
                <th>First Name</th>
                <th>Last Name</th>
                <th></th>
            </tr>
        </thead>
        <tbody>
            {
            <tr v-for="user in users" :id="user.Id">
                <td>{{user.Id}}</td>
                <td>{{user.FirstName}}</td>
                <td>{{user.LastName}}</td>
                <td>
                </td>
            </tr>
            }
        </tbody>
    </table>
</template>
<script lang="ts">
    import { Vue, Component } from 'vue-property-decorator';
    import { User, UserClient } from '../../secretsanta-client';
    @Component
    export default class UsersComponent extends Vue {
        users: User[] = null;
        async loadUsers() {
            let userClient = new UserClient();
            this.users = await userClient.getAll();
        }

        async mounted() {
            await this.loadUsers();
        }
    }
</script>