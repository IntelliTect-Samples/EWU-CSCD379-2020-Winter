<template>
    <div>
        <button class="button" @click="createGroup()">Create New</button>
        <table class="table">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Title</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="group in groups" :id="group.id" :key="group.id">
                    <td>{{group.id}}</td>
                    <td>{{group.title}}</td>
                    <td>
                        <button class="button" @click='setGroup(group)'>Edit</button>
                        <button class="button" @click='deleteGroup(group)'>Delete</button>
                    </td>
                </tr>
            </tbody>
        </table>
        <group-detail v-if="selectedGroup != null"
                                  :group="selectedGroup"
                                  @group-saved="refreshGroups()"></group-detail>
    </div>
</template>

<script lang="ts">
    import { Vue, Component } from 'vue-property-decorator'
    import { Group, GroupClient } from '../../secretsanta-client';
    import GroupDetail from './GroupDetail.vue';
    @Component({
        components: {
            GroupDetail
        }
    })
    export default class Groups extends Vue {
        groups: Group[] = null
        selectedGroup: Group = null;

        async loadGroups() {
            this.groups = await new GroupClient().getAll();
        }

        createGroup() {
            this.selectedGroup = <Group>{};
        }

        async mounted() {
            await this.loadGroups();
        }

        setGroup(group: Group) {
            this.selectedGroup = group;
        }

        async refreshGroups() {
            this.selectedGroup = null;
            await this.loadGroups();
        }

        async deleteGroup(group: Group) {
                if (confirm(`Are you sure you want to delete ${group.title}?`)) {
                    await new GroupClient().delete(group.id);
                    await this.refreshGroups();
                }
        }
    }
</script>
