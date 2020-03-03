<template>
    <div>
        <button class="button" @click="createUser()">Create New</button>
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
                <tr v-for="user in users" :id="user.id" :key="user.id">
                    <td>{{user.id}}</td>
                    <td>{{user.firstName}}</td>
                    <td>{{user.lastName}}</td>
                    <td>
                        <button class="button" @click='setUser(user)'>Edit</button>
                        <button class="button" @click='deleteUser(user)'>Delete</button>
                    </td>
                </tr>
            </tbody>
        </table>
        <user-detail v-if="selectedUser != null"
                                  :user="selectedUser"
                                  @user-saved="refreshUsers()"></user-detail>
    </div>
</template>

<script lang="ts">
    import { Vue, Component } from 'vue-property-decorator'
    import { User, UserClient } from '../../secretsanta-client';
    import UserDetail from './UserDetail.vue';
    @Component({
        components: {
            UserDetail
        }
    })
    export default class Users extends Vue {
        users: User[] = null
        selectedUser: User = null;

        async loadUsers() {
            this.users = await new UserClient().getAll();
        }

        createUser() {
            this.selectedUser = <User>{};
        }

        async mounted() {
            await this.loadUsers();
        }

        setUser(user: User) {
            this.selectedUser = user;
        }

        async refreshUsers() {
            this.selectedUser = null;
            await this.loadUsers();
        }

        async deleteUser(user: User) {
                if (confirm(`Are you sure you want to delete ${user.firstName} ${user.lastName}?`)) {
                    await new UserClient().delete(user.id);
                    await this.refreshUsers();
                }
        }
    }
</script>
