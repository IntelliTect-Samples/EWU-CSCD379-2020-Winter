<template>
    <div>
        <button class=" button" @click='createGroup'>Create new</button>
        <table class="table">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Title</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>

                <tr v-for="group in groups" v-bind:id="group.id">
                    <td>{{group.id}}</td>
                    <td>{{group.title}}</td>
                    <td>
                        <button class="button" @click='setGroup(group)'>Edit</button>
                        <button class="button" @click='deleteGroup(group)'>Delete</button>
                    </td>
                </tr>

            </tbody>
        </table>
        <group-details-component v-if="selectedGroup != null"
                                 :group="selectedGroup"
                                 @group-saved="refreshGroups"></group-details-component>
    </div>
</template>

<script lang="ts">
    import { Vue, Component } from 'vue-property-decorator';
    import { Group, GroupClient } from '../../secretsanta-client';
    import GroupDetailsComponent from './groupDetailsComponent.vue';
    @Component({
        components: {
            GroupDetailsComponent
        }
    })
    export default class GroupsComponent extends Vue {
        groups: Group[] = null;
        selectedGroup: Group = null;

        async loadGroups() {
            let groupClient = new GroupClient();
            this.groups = await groupClient.getAll();
        }

        createGroup() {
            this.selectedGroup = <Group>{};
        }

        async mounted() {
            await this.loadGroups();
        }

        setGroup(group: Group) {
            let tempGroup = { ...group };
            this.selectedGroup = <Group>tempGroup;
        }

        async deleteGroup(group: Group) {
            let groupClient = new GroupClient();
            if (confirm(`Are you sure you want to delete ${group.title}`)) {
                await groupClient.delete(group.id);
            }
            await this.refreshGroups();
        }

        async refreshGroups() {
            this.selectedGroup = null;
            await this.loadGroups();
        }
    }
</script>
