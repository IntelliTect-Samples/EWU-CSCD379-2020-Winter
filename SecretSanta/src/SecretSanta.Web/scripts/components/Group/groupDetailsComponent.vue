<template>
    <div class="modal is-active">
        <div class="modal-background"></div>
        <div class="modal-content">
            <div class="field">
                <label class="label has-text-white">Title</label>
                <div class="control">
                    <input class="input" type="text" v-model="clonedGroup.title" />
                </div>
            </div>
            <div class="field is-grouped">
                <div class="control">
                    <button id="submit" class="button is-primary" @click.once="save">Submit</button>
                </div>
                <div class="control">
                    <a class="button" @click="cancel">Cancel</a>
                </div>
            </div>
        </div>
    </div>
</template>
<script lang="ts">
    import { Vue, Component, Prop, Emit } from 'vue-property-decorator'
    import { Group, GroupClient } from '../../secretsanta-client';
    declare var apiUrl: string;

    @Component
    export default class GroupDetailsComponent extends Vue {
        @Prop()
        group: Group;
        clonedGroup: Group = <Group>{};

        constructor() {
            super();
        }

        mounted() {
            let tempGroup = { ...this.group };
            this.clonedGroup = <Group>tempGroup;
        }

        @Emit('group-saved')
        async save() {
            let groupClient = new GroupClient(apiUrl);
            if (this.clonedGroup.id > 0) {
                await groupClient.put(this.clonedGroup.id, this.clonedGroup);
            }
            else {
                await groupClient.post(this.clonedGroup);
            }
        }

        @Emit('group-saved')
        cancel() {
        }
    }
</script>