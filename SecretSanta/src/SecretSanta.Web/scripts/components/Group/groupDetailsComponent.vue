<template>
    <div>
        <div class="field">
            <label class="label">Title</label>
            <div class="control">
                <input class="input" type="text" v-model="group.title" />
            </div>
        </div>
        <div class="field is-grouped">
            <div class="control">
                <button id="submit" class="button is-primary" @click.once='saveGroup'>Submit</button>
            </div>
            <div class="control">
                <a asp-action="Index" class="button is-light" @click='cancelEdit'>Cancel</a>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { Vue, Component, Prop, Emit } from 'vue-property-decorator';
    import { Group, GroupClient } from '../../secretsanta-client';
    @Component
    export default class GroupDetailsComponent extends Vue {
        @Prop()
        group: Group;
        constructor() {
            super();
        }

        mounted() {
            let tempGroup = { ...this.group };
            this.group = <Group>tempGroup;
        }

        @Emit('group-saved')
        async saveGroup() {
            let groupClient = new GroupClient();
            if (this.group.id > 0) {
                await groupClient.put(this.group.id, this.group);
            }
            else {
                await groupClient.post(this.group);
            }
        }

        @Emit('group-saved')
        cancelEdit() {
        }
    }
</script>