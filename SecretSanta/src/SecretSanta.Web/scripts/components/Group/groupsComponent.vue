<template>
    <table class="table">
        <thead>
            <tr>
                <th>Id</th>
                <th>Title</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="group in groups" :id="group.id">
                <td>{{group.id}}</td>
                <td>{{group.title}}</td>
            </tr>
        </tbody>
    </table>
</template>
<script lang="ts">
    import { Vue, Component } from 'vue-property-decorator';
    import { Group, GroupClient } from '../../secretsanta-client';
    @Component
    export default class GroupsComponent extends Vue {
        groups: Group[] = null;
        async loadGroups() {
            let groupClient = new GroupClient();
            this.groups = await groupClient.getAll();
        }

        async mounted() {
            await this.loadGroups();
        }
    }
</script>